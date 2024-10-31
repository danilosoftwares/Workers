import React, {  } from 'react';
import { Container } from './styles';
import { BoxMainProps } from './types';

export const BoxMain: React.FC<BoxMainProps> = ({children}) => {
    return (
        <Container>
          {children}
        </Container>
    );
};

