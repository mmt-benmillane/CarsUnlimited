import { Grid, Typography } from "@mui/material";
import React from "react";
import ProductCard from "../ProductCard/ProductCard";
//import styles from "./LatestProducts.module.css";

type LatestProductsProps = {
  name: string;
}

const LatestProducts = ({ name }: LatestProductsProps) => (
  <Grid container spacing={3}>
    <Grid item xs={12}>
    <Typography variant="h6" color="inherit" noWrap sx={{ borderBottom: 1, borderColor: 'divider'}} style={{textTransform: 'uppercase'}}>
        <strong>LATEST { name }</strong>
    </Typography>
    </Grid>
    <Grid item xs>
      <ProductCard />
    </Grid>
    <Grid item xs>
      <ProductCard />
    </Grid>
    <Grid item xs>
      <ProductCard />
    </Grid>
  </Grid>
);

export default LatestProducts;
