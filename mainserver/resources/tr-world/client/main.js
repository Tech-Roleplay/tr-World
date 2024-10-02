/// <reference types="@altv/types-client" />
/// <reference types="@altv/types-natives" />

import * as alt from 'alt-client';
import * as native from 'natives';


//variabels
let DISCORD_APP_ID = ""


// functions
//#region Connection
alt.on("connectionComplete", () => {
    alt.log("Client-side loaded.");
})

//#endregion

//#region OAuth2
async function getOAuth2Token() {
    try {
        let token = await alt.Discord.requestOAuth2Token(DISCORD_APP_ID)
    } catch (e) {

    }

}

//#endregion

//#region static Blips
function loadBlips() {
    createBlip(-609.01495, -599.91205, 34.67566, 835, 53, 0.7, true, "Parlament");
    createBlip(-692.8296, -636.38007, 31.55694, 855, 53, 0.7, true, "Whitehouse");

    createBlip(-1277.3447, -560.0572, 30.241001, 764, 23, 0.7, true, "LS Townhall");

    // IAA
    createBlip(91.77598, -669.6379, 44.22036, 788, 7, 0.7, true, "IAA & SIU");
    createBlip(99.99243, -744.5883, 45.754734, 419, 7, 0.7, true, "FIB");
    createBlip(236.9338, -407.2875, 47.924366, 419, 0, 0.7, true, "Department of Justice - San Andreas");

    //DOJ
    createBlip(433.15298, -981.9341, 30.710115, 526, 29, 0.7, true, "LSPD");
    createBlip(359.48407, -1583.4907, 29.291954, 526, 28, 0.7, true, "LSSD");
    createBlip(1858.5795, 3677.6748, 33.69357, 526, 28, 0.7, true, "BCSO");
    createBlip(-1110.2585, -846.3497, 19.31684, 526, 29, 0.7, true, "LSPD");
    createBlip(-1629.9904, -1010.5518, 13.101151, 526, 29, 0.7, true, "LSPD");
    createBlip(-1311.5162, -1530.3833, 4.558398, 526, 29, 0.7, true, "LSPD");
    createBlip(917.5098, 3576.8838, 33.56183, 526, 26, 0.7, true, "SNPD");
    createBlip(-440.7782, 6018.8945, 31.492304, 526, 3, 0.7, true, "PBPD");
    createBlip(641.17017, 0.6331002, 82.7864, 526, 63, 0.7, true, "SASP"); //Vinewood
    createBlip(822.751, -1289.806, 28.240631, 526, 39, 0.7, true, "SAHP");
    createBlip(1852.4473, 2585.813, 45.672016, 285, 62, 0.7, true, "SA Prison");
    createBlip(376.33688, 779.18384, 185.17918, 526, 52, 0.7, true, "SAPR");
    createBlip(-560.2198, -135.14146, 38.152267, 526, 29, 0.7, true, "LSPD");

    // Fire Department
    createBlip(-379.78036, 6118.821, 31.210928, 648, 1, 0.7, true, "Fire Station");
    createBlip(1697.5897, 3586.9915, 35.089474, 648, 1, 0.7, true, "Fire Station");
    createBlip(213.33661, -1645.3972, 29.803219, 648, 1, 0.7, true, "Fire Station");
    createBlip(1202.7572, -1463.9777, 34.876827, 648, 1, 0.7, true, "Fire Station");
    createBlip(-1035.6283, -2383.6064, 14.094898, 648, 1, 0.7, true, "Fire Station");
    createBlip(-661.1193, -75.9205, 38.591236, 648, 1, 0.7, true, "Fire Station");

    // EMS
    createBlip(299.1843, -584.83685, 43.260838, 61, 11, 0.7, true, "Hospital");
    createBlip(343.9636, -1398.7815, 32.509254, 61, 11, 0.7, true, "Hospital");
    createBlip(-449.8421, -341.88446, 33.979736, 61, 11, 0.7, true, "Hospital");
    createBlip(1839.3762, 3672.352, 33.754364, 61, 11, 0.7, true, "Hospital");
    createBlip(-248.05325, 6331.792, 31.904264, 61, 11, 0.7, true, "Hospital");
    createBlip(1151.2819, -1529.2511, 35.36556, 61, 11, 0.7, true, "Hospital");

    //Bus
    createBlip(454.01633, -658.4551, 27.866255, 513, 47, 0.7, true, "Bus Station");

    // MEtro
    createBlip(-1084.2657, -2719.5398, -7.410132, 607, 49, 0.6, true, "Metro LSIA Terminal");
    createBlip(-883.624, -2314.9814, -11.732785, 607, 49, 0.6, true, "Metro LSIA Parking");
    createBlip(-539.3826, -1283.5278, 26.901606, 607, 49, 0.6, true, "Metro Puerto del Sol");
    createBlip(278.6903, -1201.9535, 38.896255, 607, 49, 0.6, true, "Metro Strawberry");
    createBlip(-292.98038, -332.386, 10.063103, 607, 49, 0.6, true, "Metro Burton");
    createBlip(-815.5151, -135.68059, 19.950294, 607, 49, 0.6, true, "Metro Portola Drive");
    createBlip(-1353.9574, -464.19272, 15.04531, 607, 49, 0.6, true, "Metro Del Perro");
    createBlip(-503.02167, -674.93274, 11.808959, 607, 49, 0.6, true, "Metro Little Seoul");
    createBlip(-212.7016, -1035.0039, 30.139433, 607, 49, 0.6, true, "Metro Pillbox South");
    createBlip(115.049774, -1727.3262, 30.110792, 607, 49, 0.6, true, "Metro Davis");

    //RecyleJob
    createBlip(115.049774, -1727.3262, 30.110792, 607, 49, 0.6, true, "Metro Davis");

    // AmmoNation

    //LS Coustomes
    createBlip(-342.38242, -136.14066, 38.3114, 72, 4, 0.6, true, "LS Customs");
    createBlip(732.34283, -1085.4066, 21.461548, 72, 4, 0.6, true, "LS Customs");
    createBlip(-1154.7296, -2006.3077, 12.480591, 72, 4, 0.6, true, "LS Customs");
    createBlip(110.4, 6626.0176, 31.082764, 72, 4, 0.6, true, "Beekers Garage & Parts");
    createBlip(1174.8264, 2640.844, 37.064453, 72, 4, 0.6, true, "Auto Repairs")

    // PDM
    createBlip(-38.795605, -1109.6835, 26.432251, 369, 13, 0.6, true, "Premium Deluxe Motorsport");

}

/**
 * Creates a blip at the given coordinates.
 * 
 * @param {number} x - The x coordinate.
 * @param {number} y - The y coordinate. 
 * @param {number} z - The z coordinate.
 * @param {number} sprite - The sprite for the blip. 
 * @param {number} color - The color for the blip.
 * @param {number} [scale=0.7] - The scale for the blip.
 * @param {boolean} [shortRange=false] - Whether this is a short range blip.
 * @param {string} [name=''] - The name for the blip.
 */

function createBlip(x, y, z, sprite, color, scale = 1.0, shortRange = false, name = "") {
    let tempBlip = new alt.PointBlip(x, y, z);

    tempBlip.sprite = sprite;
    tempBlip.color = color;
    tempBlip.scale = scale;
    tempBlip.shortRange = shortRange;
    if (name.length > 0) {
        tempBlip.name = name;
    }

}
//#endregion

alt.onServer("SetPlayerIntoVehicle", (vehicle, seat) => {
    if (!vehicle) return // it was destroyed

    native.setPedIntoVehicle(
        alt.Player.local, vehicle, -1 // altv and game natives seat offset
    )
})