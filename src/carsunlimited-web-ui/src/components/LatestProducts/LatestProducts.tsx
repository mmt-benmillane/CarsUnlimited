import { Grid, Typography } from "@mui/material";
import React from "react";
import axios from "axios";
import { useQuery } from "react-query";
import InventoryItem from "../../models/InventoryItem.d";
import ProductCard from "../ProductCard/ProductCard";

//import styles from "./LatestProducts.module.css";

type LatestProductsProps = {
  category: string;
};

const API_URL = process.env.REACT_APP_API_URL;
 
// const getInventoryImage = (images = []) => {
//   return images.find(image => image.isPrimary) || images[0];
// };


const LatestProducts = ({category}: LatestProductsProps) => {
  const fetchLatestProducts = async () => {
    const response = await axios.get(
      `${API_URL}/Inventory/${category}/latest`
    );
    return response.data;
  };

  const { isLoading, error, data } = useQuery("latestProducts", fetchLatestProducts);
  if (isLoading) {
    return <div>Loading...</div>;
  } 
  if (error) {
    return <div>Error!</div>;
  }

  return data?.map((product: InventoryItem) => (
    <Grid item xs>
      <ProductCard item={product} />
    </Grid>
  ));
  
}

const LatestProductsComponent = ({category}: LatestProductsProps) => {
  return (
    <Grid container spacing={3}>
      <Grid item xs={12}>
        <Typography
          variant="h6"
          color="inherit"
          noWrap
          sx={{ borderBottom: 1, borderColor: "divider" }}
          style={{ textTransform: "uppercase" }}
        >
          <strong>LATEST {category}</strong>
        </Typography>
      </Grid>
      <LatestProducts category={category} />
    </Grid>
  );
};

export default LatestProductsComponent;
