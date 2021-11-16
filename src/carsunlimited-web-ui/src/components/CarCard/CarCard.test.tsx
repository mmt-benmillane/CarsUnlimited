import React from 'react';
import { render, screen } from '@testing-library/react';
import '@testing-library/jest-dom/extend-expect';
import CarCard from './CarCard';

describe('<CarCard />', () => {
  test('it should mount', () => {
    render(<CarCard />);
    
    const carCard = screen.getByTestId('CarCard');

    expect(carCard).toBeInTheDocument();
  });
});