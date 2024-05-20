import ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { CKEditor } from '@ckeditor/ckeditor5-react';

const Dashboard = () => {
    return (
        <div>
            dashboard
            <div className="App">
                <h2>Using CKEditor&nbsp;5 from source in React</h2>
                <CKEditor
                    editor={ClassicEditor}
                    config={
                      {
                        language: 'en',
                        initialData: {
                          minHeight: '500px'
                        }

                      }
                    }
                    data="<p>Hello from CKEditor&nbsp;5!</p>"
                    onReady={(editor) => {
                        // You can store the "editor" and use when it is needed.
                        console.log('Editor is ready to use!', editor);
                    }}
                    onChange={(event) => {
                        console.log(event);
                    }}
                    onBlur={(event, editor) => {
                        console.log('Blur.', editor);
                    }}
                    onFocus={(event, editor) => {
                        console.log('Focus.', editor);
                    }}
                />
            </div>
        </div>
    );
};

export default Dashboard;
