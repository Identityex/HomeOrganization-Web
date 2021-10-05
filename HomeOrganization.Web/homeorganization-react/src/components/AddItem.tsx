import React, {Component} from "react";
import authService from "./api-authorization/AuthorizeService";
import {isMobile} from 'react-device-detect';
import {IProps} from "../Interfaces/IProps";
import {IState} from "../Interfaces/IState";
import {WebcamCapture} from "./WebcamCapture";

export class AddItem extends Component<IProps, IState> {
    static displayName = AddItem.name;
    
    constructor(props: IProps) {
        super(props);
        this.getImage = this.getImage.bind(this);
    }
    
    video_reference: HTMLVideoElement | null = null
    set_video_reference = (element: HTMLVideoElement) => {
        this.video_reference = element
    }
    
    canvas_reference: HTMLCanvasElement | null = null;
    set_canvas_reference = (element: HTMLCanvasElement) => {
        this.canvas_reference = element;
    }
    
    async componentDidMount() {
        if (this.video_reference) {
            this.video_reference.srcObject = await navigator.mediaDevices.getUserMedia({
                video: { facingMode: "environment"},
                audio: false
            })
        }
    }

    
    async sendDataToServer(imageName : string, base64Data: string)
    {

        const token = await authService.getAccessToken();
        const response = await fetch('UploadPhoto', {
            headers: !token ? {} : { 'Authorization': `Bearer ${token}` },
            method: 'POST',
            body: JSON.stringify({imageName : imageName, image: base64Data})
        });
    }
    showBadge(msg: string)
    {

    }
    
    render() {
        return this.getImage();
    }
    
    getImage() {
        
        if(isMobile)
        {
            return (
                <h3 className="alert-warning">Please use this on a mobile device.</h3>
            );
        }

        return (
            <div>
                <WebcamCapture />
            </div>
        );
    }
}
