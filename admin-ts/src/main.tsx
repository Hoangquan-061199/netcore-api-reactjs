import React from 'react';
import ReactDOM from 'react-dom/client';
import { BrowserRouter } from 'react-router-dom';
import Routers from './routes/Routers.tsx';
import './assets/styles/base.scss';
import './assets/styles/style.scss';
import { Provider } from 'react-redux';
import { persistor, store } from './redux/store.ts';
import { PersistGate } from 'redux-persist/integration/react';

ReactDOM.createRoot(document.getElementById('root')!).render(
    <React.StrictMode>
        <BrowserRouter>
            <Provider store={store}>
                <PersistGate loading={<div>loading...</div>} persistor={persistor}>
                    <Routers />
                </PersistGate>
            </Provider>
        </BrowserRouter>
    </React.StrictMode>
);
