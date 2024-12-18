import BadgeStyle from "../enums/BadgeStyle.js";
import ItemsCollection from "../modules/ItemsCollection.js";
import ListItem from "../modules/ListItem.js";
import ResText from "../modules/ResText.js";
import Sprite from "../modules/Sprite.js";
import UIMenuItem from "./UIMenuItem.js";

export default class UIMenuListItem extends UIMenuItem {
    ScrollingEnabled: boolean;
    HoldTimeBeforeScroll: number;
    protected _itemText: ResText;
    protected _arrowLeft: Sprite;
    protected _arrowRight: Sprite;
    protected _index: number;
    private _currentOffset;
    private _itemsCollection;

    constructor(text: string, description?: string, collection?: ItemsCollection, startIndex?: number, data?: any);

    get Collection(): ListItem[];

    set Collection(v: ListItem[]);

    get SelectedItem(): ListItem;

    set SelectedItem(v: ListItem);

    get SelectedValue(): any;

    get Index(): number;

    set Index(value: number);

    setCollection(collection: ItemsCollection): void;

    setCollectionItem(index: number, item: ListItem | string, resetSelection?: boolean): void;

    SetVerticalPosition(y: number): void;

    SetRightLabel(text: string): this;

    SetRightBadge(badge: BadgeStyle): this;

    Draw(): void;
}
