import BadgeStyle from "../enums/BadgeStyle.js";
import NativeUI from "../NativeUi.js";
import ResRectangle from "../modules/ResRectangle.js";
import ResText from "../modules/ResText.js";
import Sprite from "../modules/Sprite.js";
import Color from "../utils/Color.js";
import Point from "../utils/Point.js";

export default class UIMenuItem {
    static readonly DefaultBackColor: Color;
    static readonly DefaultHighlightedBackColor: Color;
    static readonly DefaultForeColor: Color;
    static readonly DefaultHighlightedForeColor: Color;
    readonly Id: string;
    BackColor: Color;
    HighlightedBackColor: Color;
    ForeColor: Color;
    HighlightedForeColor: Color;
    Enabled: boolean;
    Selected: boolean;
    Hovered: boolean;
    Data: any;
    Offset: Point;
    Parent: NativeUI;
    RightLabel: string;
    LeftBadge: BadgeStyle;
    RightBadge: BadgeStyle;
    protected _rectangle: ResRectangle;
    protected _text: ResText;
    protected _description: string;
    protected _selectedSprite: Sprite;
    protected _badgeLeft: Sprite;
    protected _badgeRight: Sprite;
    protected _labelText: ResText;
    private _event;

    constructor(text: string, description?: string, data?: any);

    get Text(): string;

    set Text(text: string);

    get Description(): string;

    set Description(text: string);

    SetVerticalPosition(y: number): void;

    addEvent(event: string, ...args: any[]): void;

    fireEvent(): void;

    Draw(): void;

    SetLeftBadge(badge: BadgeStyle): void;

    SetRightBadge(badge: BadgeStyle): void;

    SetRightLabel(text: string): void;

    BadgeToSpriteLib(badge: BadgeStyle): "commonmenu" | "mpshopsale" | "mpleaderboard";

    BadgeToSpriteName(badge: BadgeStyle, selected: boolean): "" | "mp_medal_bronze" | "mp_medal_gold" | "medal_silver" | "mp_alerttriangle" | "mp_hostcrown" | "shop_ammo_icon_b" | "shop_ammo_icon_a" | "shop_armour_icon_b" | "shop_armour_icon_a" | "shop_barber_icon_b" | "shop_barber_icon_a" | "shop_clothing_icon_b" | "shop_clothing_icon_a" | "shop_franklin_icon_b" | "shop_franklin_icon_a" | "shop_garage_bike_icon_b" | "shop_garage_bike_icon_a" | "shop_garage_icon_b" | "shop_garage_icon_a" | "shop_gunclub_icon_b" | "shop_gunclub_icon_a" | "shop_health_icon_b" | "shop_health_icon_a" | "shop_lock" | "shop_makeup_icon_b" | "shop_makeup_icon_a" | "shop_mask_icon_b" | "shop_mask_icon_a" | "shop_michael_icon_b" | "shop_michael_icon_a" | "shop_new_star" | "shop_tattoos_icon_b" | "shop_tattoos_icon_a" | "shop_tick_icon" | "shop_trevor_icon_b" | "shop_trevor_icon_a" | "saleicon" | "arrowleft" | "arrowright" | "leaderboard_audio_1" | "leaderboard_audio_2" | "leaderboard_audio_3" | "leaderboard_audio_inactive" | "leaderboard_audio_mute";

    IsBagdeWhiteSprite(badge: BadgeStyle): boolean;

    BadgeToColor(badge: BadgeStyle, selected: boolean): Color;
}
