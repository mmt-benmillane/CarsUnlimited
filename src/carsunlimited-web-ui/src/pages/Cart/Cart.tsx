import { Divider, Grid } from '@mui/material';
import axios from 'axios';
import React from 'react';
import { useQuery } from 'react-query';
import Layout from '../../layouts/MainLayout';
import styles from './Cart.module.css';

import CartItem from '../../models/CartItem.d';
import CartItemCard from '../../components/CartItemCard/CartItemCard';

const CART_API_URL = process.env.REACT_APP_CART_API_URL;

const sessionId = localStorage.getItem("sessionId") || '';
const headers = {
  'X-CarsUnlimited-SessionId': sessionId
}

const fetchCartItems = async () => {
  const response = await axios.get(`${CART_API_URL}/Cart/get-cart-items`, { headers })
  return response.data
}

const CartItems = () => {
  const { isLoading, error, data } = useQuery('cart-items', fetchCartItems);

  if (isLoading) {
    return <div>Loading...</div>
  }

  if (error) {
    return <div>Error!</div>
  }

  if(data.length === 0) {
    return (
      <Grid item xs>
        No items in cart.
      </Grid>
    );
  }

  return data?.map((item: CartItem) => (
    <Grid item xs={12}>
      <CartItemCard item={item} />
      <Divider />
    </Grid>
  ));
};

const Cart = () => (
  <Layout>
    <div className={styles.Cart} data-testid="Cart">
      <h1>Cart</h1>
      <Grid container spacing={{ xs: 1 }}>
        <Grid item xs={2}></Grid>
        <Grid item xs={2}>
          <strong>Product</strong>
        </Grid>
        <Grid item xs={2}>
          <strong>Unit Price</strong>
        </Grid>
        <Grid item xs={2}>
          <strong>Quantity</strong>
        </Grid>
        <Grid item xs={2}>
          <strong>Total Price</strong>
        </Grid>
        <Grid item xs={2}></Grid>        
        <CartItems />
      </Grid>
    </div>
  </Layout>
);

export default Cart;
