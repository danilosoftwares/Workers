import React, { useState } from 'react';
import { BoxStart } from '../BoxStart';
import { LoginComponent } from '../LoginComponent';
import { Register } from '../Register';
import { ScreenInitialProps } from './types';

export const ScreenInitial: React.FC<ScreenInitialProps> = () => {
    const [isRegister, setIsRegister] = useState(false)
    return (
        <BoxStart>
        {isRegister ? <Register onLogin={()=>setIsRegister(false)} />  : <LoginComponent onRegister={()=>setIsRegister(true)} />}
        </BoxStart>
    );
};

