import React from "react";

export default interface InventoryItem {
    id: string;
    manufacturer: string;
    model: string;
    category: string;
    price: number;
    sku: string;
    description: string;    
    images?: [];
    inStock: number;
    createdDate: Date;
}