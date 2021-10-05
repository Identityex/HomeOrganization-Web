export class AuthenticationResult{
    constructor(_status: string, _message: Nullable<string> = null, _state: any = null){
        this.status = _status;
        this.message = _message;
        this.state = _state;
    }
    
    public status!: string;
    public message!: Nullable<string>;
    public state!: any;
}