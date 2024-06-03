const FileManager = () => {
    return (
        <div>
            <table cellPadding={0} cellSpacing={0} id="wraper">
                <tbody>
                    <tr>
                        <td valign="top" className="pnlDirs" id="dirActions">
                            <div className="actions">
                                <input
                                    type="button"
                                    id="btnAddDir"
                                    // defaultValue="Create"
                                    title="Create new directory"
                                    // onclick="addDir()"
                                />
                                <input
                                    type="button"
                                    id="btnRenameDir"
                                    // defaultValue="Rename"
                                    title="Rename directory"
                                    // onclick="renameDir()"
                                    data-lang-v="RenameDir"
                                    data-lang-t="T_RenameDir"
                                />
                                <input
                                    type="button"
                                    id="btnDeleteDir"
                                    // defaultValue="Delete"
                                    title="Delete selected directory"
                                    // onclick="deleteDir()"
                                    data-lang-v="DeleteDir"
                                    data-lang-t="T_DeleteDir"
                                />
                            </div>
                            <div id="pnlLoadingDirs">
                                <span>Loading directories...</span>
                                <br />
                                <img src="images/loading.gif" title="Loading directory tree, please wait..." />
                            </div>
                            <div className="scrollPane">
                                <ul id="pnlDirList" />
                            </div>
                        </td>
                        <td valign="top" id="fileActions">
                            <input
                                type="hidden"
                                id="hdViewType"
                                // defaultValue="list"
                            />
                            <input
                                type="hidden"
                                id="hdOrder"
                                // defaultValue="asc"
                            />
                            <div className="actions">
                                <input
                                    type="button"
                                    id="btnAddFile"
                                    title="Upload files"
                                    // onclick="addFileClick()"
                                    data-lang-v="AddFile"
                                    data-lang-t="T_AddFile"
                                />
                                <input
                                    type="button"
                                    id="btnPreviewFile"
                                    title="Preview selected file"
                                    // onclick="previewFile()"
                                    data-lang-v="Preview"
                                    data-lang-t="T_Preview"
                                />
                                <input
                                    type="button"
                                    id="btnRenameFile"
                                    title="Rename selected file"
                                    // onclick="renameFile()"
                                    data-lang-v="RenameFile"
                                    data-lang-t="T_RenameFile"
                                />
                                <input
                                    type="button"
                                    id="btnDownloadFile"
                                    title="Download selected file"
                                    // onclick="downloadFile()"
                                    data-lang-v="DownloadFile"
                                    data-lang-t="T_DownloadFile"
                                />
                                <input
                                    type="button"
                                    id="btnDeleteFile"
                                    title="Delete selected file"
                                    // onclick="deleteFile()"
                                    data-lang-v="DeleteFile"
                                    data-lang-t="T_DeleteFile"
                                />
                                <input
                                    type="button"
                                    id="btnSelectAllFile"
                                    // defaultValue="Chọn tất cả"
                                    title="Chọn tất cả"
                                    // onclick="setAllFile()"
                                />
                                <input
                                    type="button"
                                    id="btnUnSelectAllFile"
                                    // defaultValue="Bỏ chọn tất cả"
                                    title="Bỏ chọn tất cả"
                                    // onclick="unSetAllFile()"
                                />
                                <input
                                    type="button"
                                    id="btnSelectFile"
                                    title="Select highlighted file"
                                    // onclick="setFile()"
                                    data-lang-v="SelectFile"
                                    data-lang-t="T_SelectFile"
                                />
                                <span className="order" data-lang="OrderBy">
                                    Order by
                                </span>
                                :
                                <select
                                    id="ddlOrder"
                                    defaultValue={'name'}
                                    // onchange="sortFiles()"
                                >
                                    <option value="name" data-lang="Name_asc">
                                        Name
                                    </option>
                                    <option value="size" data-lang="Size_asc">
                                        Size
                                    </option>
                                    <option value="time" data-lang="Date_asc">
                                        Date
                                    </option>
                                    <option value="time_desc" data-lang="Date_desc">
                                        Date
                                    </option>
                                    <option value="name_desc" data-lang="Name_desc">
                                        Name
                                    </option>
                                    <option value="size_desc" data-lang="Size_desc">
                                        Size
                                    </option>
                                </select>
                                <input
                                    type="button"
                                    id="btnListView"
                                    className="btnView"
                                    // defaultValue
                                    title="List view"
                                    // onclick="switchView('list')"
                                    data-lang-t="T_ListView"
                                />
                                <input
                                    type="button"
                                    id="btnThumbView"
                                    className="btnView"
                                    // defaultValue
                                    title="Thumbnails view"
                                    // onclick="switchView('thumb')"
                                    data-lang-t="T_ThumbsView"
                                />
                                &nbsp;&nbsp;
                                <input
                                    type="text"
                                    id="txtSearch"
                                    //  onkeyup="filterFiles()"
                                    //  onchange="filterFiles()"
                                />
                            </div>
                            <div className="pnlFiles">
                                <div className="scrollPane">
                                    <div id="pnlLoading">
                                        <span data-lang="LoadingFiles">Loading files...</span>
                                        <br />
                                        <img src="images/loading.gif" title="Loading files, please wait..." />
                                    </div>
                                    <div id="pnlEmptyDir" data-lang="DirIsEmpty">
                                        This folder is empty
                                    </div>
                                    <div id="pnlSearchNoFiles" data-lang="NoFilesFound">
                                        No files found
                                    </div>
                                    <ul id="pnlFileList" />
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td className="bottomLine"></td>
                        <td className="bottomLine">
                            <div id="pnlStatus">Status bar</div>
                        </td>
                    </tr>
                </tbody>
            </table>
            {/* Forms and other components */}
            <iframe width={0} height={0} style={{ display: 'none', border: 0 }} />
            <div id="dlgAddFile">
                <form name="addfile" id="frmUpload" method="post" target="frmUploadFile" encType="multipart/form-data">
                    <input type="hidden" name="d" id="hdDir" />
                    <div className="form">
                        <br />
                        <input
                            type="file"
                            name="files[]"
                            id="fileUploads"
                            // onchange="listUploadFiles(this.files)"
                            // multiple="multiple"
                        />
                        <div id="uploadResult" />
                        <div className="uploadFilesList">
                            <div id="uploadFilesList" />
                        </div>
                    </div>
                </form>
            </div>
            <div id="menuFile" className="contextMenu">
                <a
                    href="#"
                    // onclick="setFile()"
                    data-lang="SelectFile"
                    id="mnuSelectFile">
                    Select
                </a>
                <hr />
                <a
                    href="#"
                    // onclick="previewFile()"
                    data-lang="Preview"
                    id="mnuPreview">
                    Preview
                </a>
                <hr />
                <a
                    href="#"
                    //  onclick="downloadFile()"
                    data-lang="DownloadFile"
                    id="mnuDownload">
                    Download
                </a>
                <hr />
                <a
                    href="#"
                    // onclick="return pasteToFiles(event, this)"
                    data-lang="Paste"
                    className="paste pale"
                    id="mnuFilePaste">
                    Paste
                </a>
                <hr />
                <a
                    href="#"
                    //  onclick="cutFile()"
                    data-lang="Cut"
                    id="mnuFileCut">
                    Cut
                </a>
                <hr />
                <a
                    href="#"
                    //  onclick="copyFile()"
                    data-lang="Copy"
                    id="mnuFileCopy">
                    Copy
                </a>
                <hr />
                <a
                    href="#"
                    // onclick="renameFile()"
                    data-lang="RenameFile"
                    id="mnuRenameFile">
                    Rename
                </a>
                <hr />
                <a
                    href="#"
                    // onclick="deleteFile()"
                    data-lang="DeleteFile"
                    id="mnuDeleteFile">
                    Delete
                </a>
                {/* hr>
    <a href="#" onclick="fileProperties()" id="mnuProp">Properties</a */}
            </div>
            <div id="menuDir" className="contextMenu">
                <a
                    href="#"
                    // onclick="downloadDir()"
                    data-lang="Download"
                    id="mnuDownloadDir">
                    Download
                </a>
                <hr />
                <a
                    href="#"
                    // onclick="addDir()"
                    data-lang="T_CreateDir"
                    id="mnuCreateDir">
                    Create new
                </a>
                <hr />
                <a
                    href="#"
                    // onclick="return pasteToDirs(event, this)"
                    data-lang="Paste"
                    className="paste pale"
                    id="mnuDirPaste">
                    Paste
                </a>
                <hr />
                <a
                    href="#"
                    //  onclick="cutDir()"
                    data-lang="Cut"
                    id="mnuDirCut">
                    Cut
                </a>
                <hr />
                <a
                    href="#"
                    // onclick="copyDir()"
                    data-lang="Copy"
                    id="mnuDirCopy">
                    Copy
                </a>
                <hr />
                <a
                    href="#"
                    // onclick="renameDir()"
                    data-lang="RenameDir"
                    id="mnuRenameDir">
                    Rename
                </a>
                <hr />
                <a
                    href="#"
                    // onclick="deleteDir()"
                    data-lang="DeleteDir"
                    id="mnuDeleteDir">
                    Delete
                </a>
            </div>
            <div id="pnlRenameFile" className="dialog">
                <span className="name" />
                <br />
                <input type="text" id="txtFileName" />
            </div>
            <div id="pnlDirName" className="dialog">
                <span className="name" />
                <br />
                <input type="text" id="txtDirName" />
            </div>
        </div>
    );
};

export default FileManager;
