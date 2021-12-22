import '../../helpers/FontAwesome';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Button } from '@mui/material';
import React, { Component } from 'react';
import axios from 'axios';

const API_URL = process.env.REACT_APP_CART_API_URL;
const sessionId = localStorage.getItem("sessionId") || '';
const headers = {
  'X-CarsUnlimited-SessionId': sessionId
}

class CartButton extends Component {
  state = {
    itemCount: 0
  }

  componentDidMount() {
    axios.get(`${API_URL}/Cart/get-cart-items-count`, { headers })
      .then(response => {
        this.setState({ itemCount: response.data });
      })
      .catch(error => {
        console.error('An error occurred!', error);
      });
  }

  render() {
    return (
      <div>
        <Button variant="outlined" startIcon={<FontAwesomeIcon icon={['fas', 'shopping-basket']} />}>
          {this.state.itemCount}
        </Button>
      </div>
    );
  }
}


export default CartButton;
