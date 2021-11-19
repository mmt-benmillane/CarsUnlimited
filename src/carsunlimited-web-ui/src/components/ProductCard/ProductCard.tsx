import React from 'react';
//import styles from './ProductCard.module.css';

import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import CardMedia from '@mui/material/CardMedia';
import Typography from '@mui/material/Typography';

export default function ProductCard() {
  return (
    <Card sx={{ maxWidth: 345 }}>
      <CardMedia
        component="img"
        height="200"
        image="https://via.placeholder.com/300x200"
        alt="Placeholder"
      />
      <CardContent>
        <Typography gutterBottom variant="h6" component="div">
          [[Some Car Name]]
        </Typography>
        <Typography variant="body2" color="text.secondary">
          Lorem ipsum dolor sit amet consectetur adipisicing elit.
        </Typography>
      </CardContent>
    </Card>
  );
}
