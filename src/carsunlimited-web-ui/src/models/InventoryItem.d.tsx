import React from "react";

export default interface InventoryItem {
    id: string;
    manufacturer: string;
    model: string;
    category: string;
    price: number;
    sku: string;
    description: string;    
    images: InventoryImage[];
    inStock: number;
    createdDate: Date;
}

export interface InventoryImage {
    imageUrl: string;
    isPrimary: boolean;
}