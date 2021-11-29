import React from "react";
import styles from './ProductCard.module.css';

import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import CardMedia from "@mui/material/CardMedia";
import Typography from "@mui/material/Typography";
import Rating from "../Rating/Rating";

import { Link } from "react-router-dom";

type Props = {
  manufacturer: string,
  model: string
}

export default function ProductCard({ manufacturer, model }: Props) {

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
            <Rating showLabel={false} />
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
