import Color from "../utils/Color.js";
import Point from "../utils/Point.js";
import IElement from "./IElement.js";
import Size from '../utils/Size.js';

export default class Text extends IElement {
    Caption: string;
    Pos: Point;
    Scale: number;
    Color: Color;
    Font: number;
    Centered: boolean;

    constructor(caption: string, pos: Point, scale: number, color: Color, font: number, centered: boolean);

    static AddLongString(text: string): void;

    Draw(caption: Size, pos: Point, scale: number, color: Color, font: string | number, centered: boolean): void;
}
export {Text};
