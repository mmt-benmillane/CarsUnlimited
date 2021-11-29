import React from "react";

export class InventoryItem {
    id!: string;
    manufacturer!: string;
    model!: string;
    category!: string;
    price!: number;
    sku!: string;
    description!: string;
    images!: InventoryImage[];
    inStock!: number;
    createdDate!: Date;
}

type InventoryImage = {
    imageUrl: string;
    isPrimary: boolean;
}