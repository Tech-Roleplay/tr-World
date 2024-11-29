/// <reference types="@altv/types-client" />
/// <reference types="@altv/types-natives" />

// @ts-ignore
import * as alt from 'alt-client';
// @ts-ignore
import * as native from 'natives';
import { loadBlips } from './blips.js';

alt.on("resourceStart", () => {
    alt.log("Resource started");

    // import all stuff here
    // @ts-ignore
    import ("./blips.js")
    // @ts-ignore
    import ("./cayoperico.js")

    loadBlips();
})