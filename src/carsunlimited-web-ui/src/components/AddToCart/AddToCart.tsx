import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Button } from '@mui/material';
import axios from 'axios';
import React from 'react';

type AddToCartProps = {
  manufacturer: string;
  model: string;
};

const API_URL = process.env.REACT_APP_CART_API_URL;

const AddItemToCart = async ({ manufacturer, model }: AddToCartProps) => {
  
  const sessionId = localStorage.getItem("sessionId") || '';
  const headers = {
    'X-CarsUnlimited-SessionId': sessionId
  }

  console.log(`Add ${manufacturer} ${model} to cart`);
  const itemId = `${manufacturer}_${model}`;
  const cartItem = { itemid: itemId, count: 1 };
  
  await axios.post(`${API_URL}/Cart/add-to-cart`, cartItem, { headers })
              .catch(error => {
                console.error('An error occurred!', error);
              });
};

function AddToCart({ manufacturer, model }: AddToCartProps) {


  return (
    <Button
      variant="contained"
      color="primary"
      size="large"
      startIcon={<FontAwesomeIcon icon="cart-plus" />}
      onClick={() => {AddItemToCart({ manufacturer, model })}}
    >
      Add to cart
    </Button>
  );
}

export default AddToCart;
