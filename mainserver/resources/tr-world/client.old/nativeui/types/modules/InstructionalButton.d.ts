import UIMenuItem from "../items/UIMenuItem.js";
import Control from '../enums/Control.js';

export default class InstructionalButton {
    Text: string;
    private _itemBind;
    private readonly _buttonString;
    private readonly _buttonControl;
    private readonly _usingControls;

    constructor(text: string, control: Control, buttonString?: string);

    get ItemBind(): UIMenuItem;

    BindToItem(item: UIMenuItem): void;

    GetButtonId(): string;
}
