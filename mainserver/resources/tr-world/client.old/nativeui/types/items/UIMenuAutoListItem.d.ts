import BadgeStyle from "../enums/BadgeStyle.js";
import ResText from "../modules/ResText.js";
import Sprite from "../modules/Sprite.js";
import UIMenuItem from "./UIMenuItem.js";

export default class UIMenuAutoListItem extends UIMenuItem {
    protected _itemText: ResText;
    protected _arrowLeft: Sprite;
    protected _arrowRight: Sprite;
    private _currentOffset;
    private _leftMoveThreshold;
    private _rightMoveThreshold;
    private _lowerThreshold;
    private _upperThreshold;
    private _preCaptionText;
    private _postCaptionText;
    private _selectedValue;

    constructor(text: string, description?: string, lowerThreshold?: number, upperThreshold?: number, startValue?: number, data?: any);

    get PreCaptionText(): string;

    set PreCaptionText(text: string);

    get PostCaptionText(): string;

    set PostCaptionText(text: string);

    get LeftMoveThreshold(): number;

    set LeftMoveThreshold(value: number);

    get RightMoveThreshold(): number;

    set RightMoveThreshold(value: number);

    get LowerThreshold(): number;

    set LowerThreshold(value: number);

    get UpperThreshold(): number;

    set UpperThreshold(value: number);

    get SelectedValue(): number;

    set SelectedValue(value: number);

    SetVerticalPosition(y: number): void;

    SetRightLabel(text: string): this;

    SetRightBadge(badge: BadgeStyle): this;

    Draw(): void;
}
