import ListDepartments from '../../components/department/ListDepartments';

const Department = () => {
    document.title = 'Danh sách phòng ban';
    return (
        <div>
            <ListDepartments />
        </div>
    );
};

export default Department;
