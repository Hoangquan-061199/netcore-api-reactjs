import { Navigate, Outlet } from 'react-router-dom';
import { selectCurrentToken } from '../redux/auth/Auth.slice';
import { useSelector } from 'react-redux';
import './css/main.css'

const _LayoutFileManager = () => {
    const istoken = useSelector(selectCurrentToken);
    return istoken ? (
        <>
            <Outlet />
        </>
    ) : (
        <Navigate to="/login" replace={true} />
    );
};

export default _LayoutFileManager;
