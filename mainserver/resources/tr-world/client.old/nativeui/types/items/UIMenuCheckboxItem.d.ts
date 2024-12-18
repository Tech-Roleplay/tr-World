import BadgeStyle from "../enums/BadgeStyle.js";
import UIMenuItem from "./UIMenuItem.js";

export default class UIMenuCheckboxItem extends UIMenuItem {
    Checked: boolean;
    private readonly _checkedSprite;

    constructor(text: string, check?: boolean, description?: string);

    SetVerticalPosition(y: number): void;

    Draw(): void;

    SetRightBadge(badge: BadgeStyle): this;

    SetRightLabel(text: string): this;
}
