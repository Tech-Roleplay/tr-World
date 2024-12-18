import BadgeStyle from "../enums/BadgeStyle.js";
import ChangeDirection from "../enums/ChangeDirection.js";
import ResText from "../modules/ResText.js";
import Sprite from "../modules/Sprite.js";
import UIMenuItem from "./UIMenuItem.js";

interface SelectionChangeHandler {
    (item: UIMenuDynamicListItem, selectedValue: string, changeDirection: ChangeDirection): string;
}

export default class UIMenuDynamicListItem extends UIMenuItem {
    readonly SelectionChangeHandler: SelectionChangeHandler;
    protected _itemText: ResText;
    protected _arrowLeft: Sprite;
    protected _arrowRight: Sprite;
    private _currentOffset;
    private _precaptionText;
    private _selectedValue;
    private readonly _selectedStartValueHandler;
    private isVariableFunction;

    constructor(text: string, selectionChangeHandler: {
        (item: UIMenuDynamicListItem, selectedValue: string, changeDirection: ChangeDirection): string;
    }, description?: string, selectedStartValueHandler?: {
        (): string;
    }, data?: any);

    get PreCaptionText(): string;

    set PreCaptionText(text: string);

    get SelectedValue(): string;

    set SelectedValue(value: string);

    SelectionChangeHandlerPromise(item: UIMenuDynamicListItem, selectedValue: string, changeDirection: ChangeDirection): Promise<unknown>;

    SetVerticalPosition(y: number): void;

    SetRightLabel(text: string): this;

    SetRightBadge(badge: BadgeStyle): this;

    Draw(): void;
}
export {};
