import BadgeStyle from "./enums/BadgeStyle.js";
import Font from "./enums/Font.js";
import Alignment from './enums/Alignment.js';
import Control from './enums/Control.js';
import HudColor from './enums/HudColor.js';
import ChangeDirection from './enums/ChangeDirection.js';
import UIMenuCheckboxItem from "./items/UIMenuCheckboxItem.js";
import UIMenuItem from "./items/UIMenuItem.js";
import UIMenuListItem from "./items/UIMenuListItem.js";
import UIMenuAutoListItem from "./items/UIMenuAutoListItem.js";
import UIMenuSliderItem from "./items/UIMenuSliderItem.js";
import ItemsCollection from "./modules/ItemsCollection.js";
import ListItem from "./modules/ListItem.js";
import ResRectangle from "./modules/ResRectangle.js";
import ResText from "./modules/ResText.js";
import Sprite from "./modules/Sprite.js";
import Color from "./utils/Color.js";
import LiteEvent from "./utils/LiteEvent.js";
import Point from "./utils/Point.js";
import Size from "./utils/Size.js";
import InstructionalButton from './modules/InstructionalButton.js';
import BigMessage from './modules/BigMessage.js';
import MidsizedMessage from './modules/MidsizedMessage.js';
import UIMenuDynamicListItem from './items/UIMenuDynamicListItem.js';

export default class NativeUI {
    readonly Id: string;
    readonly SelectTextLocalized: string;
    readonly BackTextLocalized: string;
    WidthOffset: number;
    ParentMenu: NativeUI;
    ParentItem: UIMenuItem;
    Children: Map<string, NativeUI>;
    MouseControlsEnabled: boolean;
    CloseableByUser: boolean;
    AUDIO_LIBRARY: string;
    AUDIO_UPDOWN: string;
    AUDIO_LEFTRIGHT: string;
    AUDIO_SELECT: string;
    AUDIO_BACK: string;
    AUDIO_ERROR: string;
    MenuItems: (UIMenuItem | UIMenuListItem | UIMenuAutoListItem | UIMenuDynamicListItem | UIMenuSliderItem | UIMenuCheckboxItem)[];
    readonly IndexChange: LiteEvent;
    readonly ListChange: LiteEvent;
    readonly AutoListChange: LiteEvent;
    readonly DynamicListChange: LiteEvent;
    readonly SliderChange: LiteEvent;
    readonly CheckboxChange: LiteEvent;
    readonly ItemSelect: LiteEvent;
    readonly MenuOpen: LiteEvent;
    readonly MenuClose: LiteEvent;
    readonly MenuChange: LiteEvent;
    private _visible;
    private _counterPretext;
    private _counterOverride;
    private _spriteLibrary;
    private _spriteName;
    private _offset;
    private _lastUpDownNavigation;
    private _lastLeftRightNavigation;
    private _extraOffset;
    private _buttonsEnabled;
    private _justOpened;
    private _justOpenedFromPool;
    private _justClosedFromPool;
    private _poolOpening;
    private _safezoneOffset;
    private _activeItem;
    private _maxItemsOnScreen;
    private _minItem;
    private _maxItem;
    private _mouseEdgeEnabled;
    private _bannerSprite;
    private _bannerRectangle;
    private _recalculateDescriptionNextFrame;
    private readonly _instructionalButtons;
    private readonly _instructionalButtonsScaleform;
    private readonly _defaultTitleScale;
    private readonly _maxMenuItems;
    private readonly _mainMenu;
    private readonly _upAndDownSprite;
    private readonly _titleResText;
    private readonly _subtitleResText;
    private readonly _extraRectangleUp;
    private readonly _extraRectangleDown;
    private readonly _descriptionBar;
    private readonly _descriptionRectangle;
    private readonly _descriptionText;
    private readonly _counterText;
    private readonly _background;
    private RecalculateDescriptionPosition;
    private CleanUp;
    private render;

    constructor(title: string, subtitle: string, offset: Point, spriteLibrary?: string, spriteName?: string);

    get MaxItemsVisible(): number;

    set MaxItemsVisible(value: number);

    get Title(): string;

    set Title(text: string);

    get GetSubTitle(): ResText;

    get SubTitle(): string;

    set SubTitle(text: string);

    get Visible(): boolean;

    set Visible(toggle: boolean);

    get CurrentSelection(): number;

    set CurrentSelection(v: number);

    GetSpriteBanner(): Sprite;

    GetRectangleBanner(): ResRectangle;

    GetTitle(): ResText;

    DisableInstructionalButtons(disable: boolean): void;

    AddInstructionalButton(button: InstructionalButton): void;

    SetSpriteBannerType(spriteBanner: Sprite): void;

    SetRectangleBannerType(rectangle: ResRectangle): void;

    AddSpriteBannerType(spriteBanner: Sprite): void;

    SetNoBannerType(): void;

    RemoveInstructionalButton(button: InstructionalButton): void;

    SetMenuWidthOffset(widthOffset: number): void;

    AddItem(item: UIMenuItem): void;

    RemoveItem(item: UIMenuItem): void;

    RefreshIndex(): void;

    Clear(): void;

    Open(): void;

    Close(closeChildren?: boolean): void;

    GoLeft(): void;

    GoRight(): void;

    SelectItem(): void;

    HasCurrentSelectionChildren(): boolean;

    IsMouseInListItemArrows(item: UIMenuItem, topLeft: Point, safezone: any): 0 | 1 | 2;

    ProcessMouse(): void;

    ProcessControl(): void;

    GoUpOverflow(): void;

    GoUp(): void;

    GoDownOverflow(): void;

    GoDown(): void;

    GoBack(): void;

    BindMenuToItem(menuToBind: NativeUI, itemToBindTo: UIMenuItem): void;

    AddSubMenu(subMenu: NativeUI, itemToBindTo: UIMenuItem): void;

    ReleaseMenuFromItem(releaseFrom: UIMenuItem): boolean;

    UpdateDescriptionCaption(): void;

    CalculateDescription(): void;

    UpdateScaleform(): void;
}
export {
    NativeUI as Menu,
    UIMenuItem,
    UIMenuListItem,
    UIMenuAutoListItem,
    UIMenuDynamicListItem,
    UIMenuCheckboxItem,
    UIMenuSliderItem,
    BadgeStyle,
    ChangeDirection,
    Font,
    Alignment,
    Control,
    HudColor,
    Sprite,
    ResRectangle,
    InstructionalButton,
    Point,
    Size,
    Color,
    ItemsCollection,
    ListItem,
    BigMessage,
    MidsizedMessage
};
