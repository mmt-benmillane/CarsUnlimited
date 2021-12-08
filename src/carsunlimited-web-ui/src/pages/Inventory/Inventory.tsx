import React from "react";
import Grid from '@mui/material/Grid';

import Layout from "../../layouts/MainLayout";

import styles from "./Inventory.module.css";
import ProductCard from "../../components/ProductCard/ProductCard";

type Props = {
  category?: string
};

const Inventory = ({ category = "Car"}: Props) => (
  <Layout>
    <div className={styles.Inventory} data-testid="Inventory">
      <h1>{ category }</h1>
      <Grid container spacing={{ xs: 2, md: 3 }} columns={{ xs: 4, sm: 8, md: 12 }}>
        {Array.from(Array(15)).map((_, index) => (
          <Grid item xs={2} sm={4} md={4} key={index}>
            {/* <ProductCard manufacturer="BMW" model="X5" /> */}
          </Grid>
        ))}
      </Grid>
    </div>
  </Layout>
);

export default Inventory;
