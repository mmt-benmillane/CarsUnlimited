import React from 'react';

import Layout from '../../layouts/MainLayout';

//import CssBaseline from '@mui/material/CssBaseline';
import styles from './HomePage.module.css';
import Carousel from './../../components/Carousel/Carousel';
import { Grid } from '@mui/material';
import LatestProducts from '../../components/LatestProducts/LatestProducts';

const HomePage = () => (

  <Layout>
    <div className={styles.HomePage} data-testid="HomePage">    
      <Grid container spacing={3}>
        <Grid item xs={12}>
          <Carousel />
        </Grid>              
        <Grid item xs={12}>
          <LatestProducts name="Cars" />
        </Grid>
        <Grid item xs={12}>
          <LatestProducts name="Parts & Accessories" />
        </Grid>
        <Grid item xs={12}>
          <LatestProducts name="Offers" />
        </Grid>
      </Grid>
    </div>
  </Layout>
);

export default HomePage;
