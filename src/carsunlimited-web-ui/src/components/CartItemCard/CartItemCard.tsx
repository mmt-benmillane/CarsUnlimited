import axios from 'axios';
import React from 'react';
import { useQuery } from 'react-query';
import CartItem from '../../models/CartItem.d';
import { InventoryItem, InventoryImage } from '../../models/InventoryItem.d';
import styles from './CartItemCard.module.css';
import { plainToClass } from 'class-transformer';
import { Button, Grid } from '@mui/material';

type Props = {
  item: CartItem;
};

const API_URL = process.env.REACT_APP_INVENTORY_API_URL;

const getInventoryItem = async (id: string) => {
  const response = await axios.get(`${API_URL}/Inventory/${id}`);
  return response.data;
};

const getInventoryImage = (images: InventoryImage[]) => {
  return images.find((image: InventoryImage) => image.isPrimary) || images[0] || "https://dummyimage.com/300x200/eee/aaa.png&text=No+image+available";
};

const CartItemCard = ({ item }: Props) => {
  const {isLoading, error, data} = useQuery(`inventory-item-${item.id}`, () => getInventoryItem(item.id));
  
  if (isLoading) {
    return <div>Loading...</div>
  }

  if (error) {
    return <div>Error!</div>
  }

  if(data.length === 0) {
    return <div>Product not found</div>
  }

  const inventoryItem = plainToClass(InventoryItem, data);

  return (
    <div className={styles.CartItemCard}>
      <Grid container spacing={1}>
        <Grid item xs={2}>
          <img src={getInventoryImage(inventoryItem.images).imageUrl} alt="Product" width={64} />
        </Grid>
        <Grid item xs={2}>
          {inventoryItem.manufacturer} {inventoryItem.model}
        </Grid>
        <Grid item xs={2}>
          &pound;{inventoryItem.price.toLocaleString(navigator.language, { minimumFractionDigits: 2 })}          
        </Grid>
        <Grid item xs={2}>
          {item.count}
        </Grid>
        <Grid item xs={2}>
          &pound;{(inventoryItem.price * item.count).toLocaleString(navigator.language, { minimumFractionDigits: 2 })}
        </Grid>
        <Grid item xs={2}>
          <Button>Remove</Button>
        </Grid>
      </Grid>
    </div>
  );
};

export default CartItemCard;
