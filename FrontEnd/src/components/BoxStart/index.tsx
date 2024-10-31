import React, {  } from 'react';
import { Container } from './styles';
import { BoxStartProps } from './types';

export const BoxStart: React.FC<BoxStartProps> = ({children}) => {
    return (
        <Container style={{ backgroundImage: `url(${"background-start.jpg"})` }}>
          {children}
        </Container>
    );
};

