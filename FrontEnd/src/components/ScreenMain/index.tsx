import React from 'react';
import { BoxMain } from '../BoxMain';
import { ListItems } from '../ListItems';
import { EditItem } from '../EditItem';
import { HashRouter, Route, Routes } from 'react-router-dom';

export const ScreenMain: React.FC = () => {
    return (
        <HashRouter>
            <Routes>
                <Route path="/" element={<BoxMain><ListItems /></BoxMain>} />
                <Route path="/edit/:id" element={<BoxMain><EditItem /></BoxMain>} />
            </Routes>
        </HashRouter>
    );
};

