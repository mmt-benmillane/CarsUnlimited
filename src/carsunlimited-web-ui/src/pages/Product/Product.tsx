import { Grid, Skeleton, Typography, Button, Stack, Avatar } from "@mui/material";
import React from "react";
import { useParams } from "react-router-dom";
import Layout from "../../layouts/MainLayout";

import "../../helpers/FontAwesome";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

import styles from "./Product.module.css";
import {  blue, deepPurple, green, grey, deepOrange } from "@mui/material/colors";
import Rating from "../../components/Rating/Rating";

function Product() {
  let { manufacturer, model } = useParams();

  return (
    <Layout>
      <br />
      <div className={styles.Product} data-testid="Product">
        <Grid container spacing={0}>
          <Grid item xs={7}>
            <Skeleton variant="rectangular" width={600} height={400} />
          </Grid>
          <Grid item xs={5}>
            <Grid container spacing={2}>
              <Grid item xs={12}>
                <h1>
                  {manufacturer} {model}
                </h1>
              </Grid>
              <Grid item xs={12}>
                <strong>Manufacturer:</strong> {manufacturer}
                <Rating />
              </Grid>
              <Grid item xs={12}>
                Available colours: <br />
                <Stack direction="row" spacing={1}>
                  <Avatar sx={{ bgcolor: grey[500]}}>&nbsp;</Avatar>
                  <Avatar sx={{ bgcolor: deepPurple[500]}}>&nbsp;</Avatar>
                  <Avatar sx={{ bgcolor: green[800]}}>&nbsp;</Avatar>
                  <Avatar sx={{ bgcolor: blue[800]}}>&nbsp;</Avatar>
                  <Avatar sx={{ bgcolor: deepOrange[800]}}>&nbsp;</Avatar>
                </Stack>
              </Grid>
              <Grid item xs={12}>
                <br />
                <Typography variant="h3" gutterBottom component="div">
                  &pound; 50,000
                </Typography>
              </Grid>
              <Grid item xs={12}>
                <Button
                  variant="contained"
                  color="primary"
                  size="large"
                  startIcon={<FontAwesomeIcon icon="cart-plus" />}
                >
                  Add to cart
                </Button>
              </Grid>
            </Grid>
          </Grid>
        </Grid>
      </div>
    </Layout>
  );
}
export default Product;
