import React from "react";
import Grid from '@mui/material/Grid';

import Layout from "../../layouts/MainLayout";

import styles from "./Inventory.module.css";
import axios from "axios";
import { useQuery } from "react-query";
import ProductCard from "../../components/ProductCard/ProductCard";
import InventoryItem from "../../models/InventoryItem.d";

type Props = {
  category?: string
  displayName?: string
};

const API_URL = process.env.REACT_APP_API_URL;

const fetchProducts = async (category: string) => {
  const response = await axios.get(
    `${API_URL}/Inventory/${category}`
  );
  return response.data;
};

const Products = ({category = "Car"}: Props) => {
  const { isLoading, error, data } = useQuery(`${category}-products`, () => fetchProducts(category));

  if (isLoading) {
    return <span>Loading...</span>;
  } 
  if (error) {
    return (
      <Grid item xs>
        Error!
      </Grid>
    );
  }
  if(data.length === 0) {
    return (
      <Grid item xs>
        No products to display
      </Grid>
    );
  }

  return data?.map((product: InventoryItem, index: number) => (
    <Grid item xs={2} sm={4} md={4}>
      <ProductCard item={product} />
    </Grid>
  ));
};

const Inventory = ({ category = "Car", displayName = "Cars"}: Props) => (
  <Layout>
    <div className={styles.Inventory} data-testid="Inventory">
      <h1>{ displayName }</h1>
      <Grid container spacing={{ xs: 2, md: 3 }} columns={{ xs: 4, sm: 8, md: 12 }}>
          <Products category={category} />
      </Grid>
    </div>
  </Layout>
);

export default Inventory;
