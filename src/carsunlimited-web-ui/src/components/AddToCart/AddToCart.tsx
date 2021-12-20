import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Button } from '@mui/material';
import React from 'react';

type AddToCartProps = {
  manufacturer: string;
  model: string;
};

const HandleClick = ({ manufacturer, model }: AddToCartProps) => {
  console.log(`Add ${manufacturer} ${model} to cart`);
};

function AddToCart({ manufacturer, model }: AddToCartProps) {


  return (
    <Button
      variant="contained"
      color="primary"
      size="large"
      startIcon={<FontAwesomeIcon icon="cart-plus" />}
      onClick={() => {HandleClick({ manufacturer, model })}}
    >
      Add to cart
    </Button>
  );
}



export default AddToCart;
