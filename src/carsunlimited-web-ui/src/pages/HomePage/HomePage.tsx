import React from 'react';
//import CssBaseline from '@mui/material/CssBaseline';
import styles from './HomePage.module.css';
import Header from './../../components/Header/Header';
import Carousel from './../../components/Carousel/Carousel';
import Navigation from '../../components/Navigation/Navigation';
import { Grid } from '@mui/material';

const HomePage = () => (

  <div className={styles.HomePage} data-testid="HomePage">    
    <Header />
    <Grid container spacing={0}>
      <Grid item xs={12}>
        <Navigation />
      </Grid>
      <Grid item xs={12}>
        <Carousel />
      </Grid>
    </Grid>
    HomePage Component
  </div>
);

export default HomePage;
