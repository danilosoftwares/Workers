import React, { useEffect, useState } from 'react';
import { ScreenMain } from '../ScreenMain';
import { ScreenInitial } from '../ScreenInitial';

export const ScreenStart: React.FC = () => {
    const [access, setAccess] = useState(false);

    const validateAccess = () => {
        const token = localStorage.getItem('auth');
        if (token){
            setAccess(true);
        }
    }

    useEffect(() => {
        validateAccess();
    }, []);

    return (
        access ? <ScreenMain /> : <ScreenInitial />
    );
};

