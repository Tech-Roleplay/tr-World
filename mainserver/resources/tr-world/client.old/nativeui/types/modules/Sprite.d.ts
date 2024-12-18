import Color from "../utils/Color.js";
import Point from "../utils/Point.js";
import Size from "../utils/Size.js";

export default class Sprite {
    TextureName: string;
    Pos: Point;
    Size: Size;
    Heading: number;
    Color: Color;
    Visible: boolean;
    private _textureDict;
    private requestTextureDictPromise;

    constructor(textureDict: string, textureName: string, pos: Point, size: Size, heading?: number, color?: Color);

    get TextureDict(): string;

    set TextureDict(v: string);

    get IsTextureDictionaryLoaded(): boolean;

    LoadTextureDictionary(): void;

    Draw(textureDictionary?: string, textureName?: string, pos?: Point, size?: Size, heading?: number, color?: Color, loadTexture?: boolean): void;
}
