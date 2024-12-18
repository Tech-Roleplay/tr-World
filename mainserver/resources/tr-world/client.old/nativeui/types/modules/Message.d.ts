import Scaleform from '../utils/Scaleform.js';

export default class Message {
    private static _messageVisible;
    private static _transitionOutTimeout;
    private static _transitionOutFinishedTimeout;
    private static _delayedTransitionInTimeout;
    private static _scaleform;
    private static _transitionOutTimeMs;
    private static _transitionOutAnimName;
    private static Load;
    private static SetDelayedTransition;
    private static TransitionIn;
    private static SetTransitionOutTimer;

    static get IsVisible(): boolean;

    protected static get Scaleform(): Scaleform;

    static ShowCustomShard(funcName: string, time?: number, ...funcArgs: any[]): void;

    static ShowComplexCustomShard(messageHandler: {
        (): void;
    }, time?: number): void;

    protected static Initialize(scaleForm: string, transitionOutAnimName: string): void;

    protected static TransitionOut(): void;

    protected static Render(): void;
}
