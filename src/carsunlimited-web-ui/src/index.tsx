import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import reportWebVitals from './reportWebVitals';

import { BrowserRouter as Router, Route, Routes } from "react-router-dom";

//pages
import { HomePage, Inventory } from './pages';

ReactDOM.render(
  <React.StrictMode>   
    <Router>
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/Cars" element={<Inventory category="Car" />} />
        <Route path="/Accessories" element={<Inventory category="Accessory" />} />
        <Route path="/Parts" element={<Inventory category="Part" />} />
      </Routes>
    </Router>
  </React.StrictMode>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
