/// <reference types="@altv/types-client" />
/// <reference types="@altv/types-natives" />

// @ts-ignore
import * as alt from 'alt-client';
// @ts-ignore
import * as NativeUI from '../nativeui/nativeui.js'


alt.on("CharSect.Show", (player, char1, char2, char3, char4, char5,) => {

    const ui = new NativeUI.Menu("Char Selection", "Select in Char", new NativeUI.Point(50, 50))
    ui.AddItem(new NativeUI.UIMenuListItem(
        "Character",
        "Choose a charcater from the List",
        new NativeUI.ItemsCollection([`Character 1`, `Character 2`, `Character 3`, `Character 4`, `Character 5`])
    ));
    ui.AddItem(new NativeUI.UIMenuItem(
        "Apply", "Apply your selcted char"
    ));
    ui.RefreshIndex();
    ui.Open()

    ui.ItemSelect.on(item => {
        if (item instanceof NativeUI.UIMenuListItem) {
            alt.emitServer("Char.LoadPlayer", item.SelectedItem.DisplayText);
        }
    })

})