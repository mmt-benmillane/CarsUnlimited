import '../../helpers/FontAwesome';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Button } from '@mui/material';
import React from 'react';
import axios from 'axios';

const API_URL = process.env.REACT_APP_CART_API_URL;
const sessionId = localStorage.getItem("sessionId") || '';
const headers = {
  'X-CarsUnlimited-SessionId': sessionId
}

export default function CartButton() {
  const [cartItems, setCartItems] = React.useState(0);

  React.useEffect(() => {
    axios.get(`${API_URL}/cart/get-cart-items-count`, { headers })
      .then(res => {
        setCartItems(res.data);
      })
      .catch(err => {
        console.log(err);
      });
  }, []);

  return (
    <Button variant="outlined" startIcon={<FontAwesomeIcon icon={['fas', 'shopping-basket']} />} id='CartButton'>
      {cartItems}
    </Button>
  );
}
