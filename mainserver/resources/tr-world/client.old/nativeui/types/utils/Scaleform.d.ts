export default class Scaleform {
    private scaleForm;
    private callFunctionHead;

    constructor(scaleForm: string);

    private _handle;

    get handle(): number;

    get isValid(): boolean;

    get isLoaded(): boolean;

    callFunction(funcName: string, ...args: any[]): void;

    callFunctionReturn(funcName: string, ...args: any[]): number;

    render2D(): void;

    recreate(): void;

    destroy(): void;
}
