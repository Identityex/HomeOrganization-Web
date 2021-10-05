import React, { useState } from 'react';
import Webcam from "react-webcam";
import authService from "./api-authorization/AuthorizeService";
import "./WebcamCapture.css";
import { toast } from 'react-toastify';
const videoConstraints = {
    width: 220,
    height: 200,
    facingMode: "user"
};

export const WebcamCapture = (callback: any, deps: React.DependencyList) => {
    
    const [image,setImage]=useState<string| null | undefined>('');
    const webcamRef = React.useRef<Webcam>(null);
    const inputRef = React.useRef<HTMLInputElement>(null);


    const capture = React.useCallback(() => {
        if(webcamRef.current != null)
        {
            const imageSrc = webcamRef.current.getScreenshot();
            setImage(imageSrc)
            const name = inputRef.current?.value;
            getAndSend(name ?? '' ,imageSrc ?? '')
                .then(() => {
                    console.log('sent to server');
                    toast(" Maybe a bigger message", {
                        position: "top-center",
                        type: "success"
                    });
                });
        }
    }, deps);

    async function getAndSend(imageName :string, image: string) {
        
        const token = await authService.getAccessToken();

        const response = await fetch('UploadPhoto', {
            headers: !token ? {} : { 
                'Authorization': `Bearer ${token}` ,
                'Content-Type' : 'application/json'
            },
            method: 'POST',
            body: JSON.stringify({imageName : imageName, image: image})
        });
        toast(" Maybe a bigger message", {
            position: "top-center",
            type: "success"
        });
        
        if(response.status == 200)
        {
            toast("Success", {
                position: "bottom-center",
                type: "success"
            })
            return;
        }
        
        throw "Error could not send";
    }

    return (
        <div className="webcam-container text-center">
            <div className="webcam-img">

                {image == '' ? <Webcam
                    audio={false}
                    height={720}
                    ref={webcamRef}
                    screenshotFormat="image/jpeg"
                    width={1280}
                    videoConstraints={videoConstraints}
                /> : <img src={image ?? ''} />}
            </div>
            <div>
                {image != '' ?
                    <button onClick={(e) => {
                        e.preventDefault();
                        setImage('')
                    }}
                        className="webcam-btn">
                        Retake Image</button> :
                    <button onClick={(e) => {
                        e.preventDefault();
                        capture();
                    }}
                    className="webcam-btn btn btn-primary btn-lg align-self-center">Capture</button>
                }
            </div>
            <label>Item Name? </label>
            <input id={"ItemName"} type="text" ref={inputRef} />
        </div>
    );
};