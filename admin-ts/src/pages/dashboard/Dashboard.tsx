import Ckeditor from '../../components/ckeditor/Ckeditor';

const Dashboard = () => {

    document.title = 'Dashboard';
    return (
        <div>
            <div className="App">
                <Ckeditor id='test' />
                <Ckeditor id='test' />
                <Ckeditor id='test' />
            </div>
        </div>
    );
};

export default Dashboard;
