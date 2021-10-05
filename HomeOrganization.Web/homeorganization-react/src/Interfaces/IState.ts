export interface IState extends Partial<IMessageState>{
    
}

interface IMessageState {
    ready: boolean;
    authenticated: boolean;
    message: Nullable<string>;
    isReady: boolean;
    returnUrl: string;
    isAuthenticated: boolean;
    userName: Nullable<string>;
}