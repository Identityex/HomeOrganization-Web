import {Component} from "react";
import {Canvas} from "../components/Canvas";

export interface IProps extends Partial<IPropsPartial>{
    
}

interface IPropsPartial{
    path: string;
    component: Component;
    action: string;
    canvas: Canvas;
}