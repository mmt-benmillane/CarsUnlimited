import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Button } from '@mui/material';
import axios from 'axios';
import toast, { Toaster } from 'react-hot-toast';
import React from 'react';

type AddToCartProps = {
  manufacturer: string;
  model: string;
};

const API_URL = process.env.REACT_APP_CART_API_URL;

const notify = ({ manufacturer, model }: AddToCartProps) => toast.promise(
  AddItemToCart({ manufacturer, model }),
  {
    loading: `Adding ${manufacturer} ${model} to cart...`,
    success: `Added ${manufacturer} ${model} to cart!`,
    error: `Failed to add ${manufacturer} ${model} to cart!`,
  }
)

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
    <div>
      <Button
        variant="contained"
        color="primary"
        size="large"
        startIcon={<FontAwesomeIcon icon="cart-plus" />}
        onClick={() => {notify({ manufacturer, model })}}
      >
        Add to cart
      </Button>
      <Toaster />
    </div>
  );
}

export default AddToCart;
