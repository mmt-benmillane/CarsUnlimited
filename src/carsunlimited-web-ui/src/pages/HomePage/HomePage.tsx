import React from 'react';
//import CssBaseline from '@mui/material/CssBaseline';
import styles from './HomePage.module.css';
import Header from './../../components/Header/Header';

const HomePage = () => (

  <div className={styles.HomePage} data-testid="HomePage">    
    <Header />
    HomePage Component
  </div>
);

export default HomePage;
