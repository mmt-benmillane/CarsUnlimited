import React from "react";
import styles from './ProductCard.module.css';

import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import CardMedia from "@mui/material/CardMedia";
import Typography from "@mui/material/Typography";
import Rating from "@mui/material/Rating";

import { Link } from "react-router-dom";

export default function ProductCard() {
  const ratingScore = 2;
  const ratingCount = 3;
  const manufacturer = "BMW";
  const model = "X5";

  return (
    <div className={styles.ProductCard} data-testid="ProductCard">
    <Link to={`/Product/${manufacturer}/${model}`}>
      <Card sx={{ maxWidth: 345 }}>
        <CardMedia
          component="img"
          height="200"
          image="https://via.placeholder.com/300x200"
          alt="Placeholder"
        />
        <CardContent>
          <Typography gutterBottom variant="h6" component="div">
            {manufacturer} {model}
          </Typography>
          <Typography variant="body2" color="text.secondary">
            <Rating name="read-only" value={ratingScore} readOnly />{" "}
            {ratingCount} ratings
          </Typography>
          <Typography variant="button" color="text.secondary" align="right">
            <strong>&pound;[[Price]]</strong>
          </Typography>
        </CardContent>
      </Card>
    </Link>
    </div>
  );
}
