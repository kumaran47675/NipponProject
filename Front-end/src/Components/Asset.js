import React, { useState } from "react";
import { Button, Modal } from 'antd';
import { EyeOutlined, CloseOutlined } from '@ant-design/icons';
import './Forms/Asset.css';
import { saveAs } from "file-saver";
import axios from "axios";
import { useLocation } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import { toast } from "react-toastify";
import { useNavigate } from "react-router-dom";
const UploadFileComponent = ({requestId}) => {
  const navigation=useNavigate();
  const [imgUrls, setImgUrls] = useState([]);
  const fileInputRef = React.createRef();
  const [viewModalVisible, setViewModalVisible] = useState(false);
  const [currentImageIndex, setCurrentImageIndex] = useState(0);
  const [filess,setFiless]=useState([]);
  const uploadFiles = (event) => {
    const files = event.target.files;
    setFiless(files);
    const uploadedUrls = [];
    for (let i = 0; i < files.length; i++) {
      const file = files[i];
      let reader = new FileReader();
      reader.onload = function () {
        uploadedUrls.push({ name: file.name, data: reader.result });
        if (uploadedUrls.length === files.length) {
          setImgUrls(uploadedUrls);
        }
      };
      reader.readAsDataURL(file);
    }
  };
  const Onsubmit = () => {
    console.log(filess)
    for (let index = 0; index < filess.length; index++) {
        const img = filess[index];
        const formData = new FormData();
        formData.append("formFile", img);
        formData.append("fileName",requestId+"_"+img.name);
          axios.post('https://localhost:7206/api/Uploader/UploadFile', formData)
          .then(
            axios.put(`https://localhost:7206/api/tinting/status/${requestId}`)
          )
          .catch((error) => {
            console.error("Error uploading files:", error);
          })
    }
    toast('Data saved', {
      position: "top-right",
      autoClose: 1000,
      hideProgressBar: false,
      closeOnClick: true,
      pauseOnHover: true,
      draggable: true,
      progress: undefined,
      theme: "light",
      });
      navigation('/tintingtable');
  };
  
  
  
 
  const resetUpload = (index) => {
    const updatedUrls = [...imgUrls];
    updatedUrls.splice(index, 1);
    setImgUrls(updatedUrls);
  };
 
  const openImageViewer = (index) => {
    setCurrentImageIndex(index);
    setViewModalVisible(true);
  };
 
  const closeImageViewer = () => {
    setViewModalVisible(false);
  };
 
  const downloadAllFiles = () => {
    imgUrls.forEach((img, index) => {
      const blob = dataURLtoBlob(img.data);
      saveAs(blob, img.name);
    });
  };
 
  const dataURLtoBlob = (dataURL) => {
    const parts = dataURL.split(';base64,');
    const contentType = parts[0].split(':')[1];
    const raw = window.atob(parts[1]);
    const rawLength = raw.length;
    const uInt8Array = new Uint8Array(rawLength);
 
    for (let i = 0; i < rawLength; i++) {
      uInt8Array[i] = raw.charCodeAt(i);
    }
 
    return new Blob([uInt8Array], { type: contentType });
  };
 
  return (
    <div>
      <div className="upload-section">
        <h2 className="text-center">Please upload the images</h2>
        <input
          type="file"
          onChange={uploadFiles}
          style={{ display: 'none' }}
          ref={fileInputRef}
          multiple
        />
        <Button type="primary" className="custom-upload-button" onClick={() => fileInputRef.current.click()}>
          Choose Files
        </Button>
        <span className="file-label">
          {imgUrls.length > 0 ? imgUrls.length + ' file(s) chosen' : 'No files chosen'}
        </span>
        <div className="uploaded-files">
          {imgUrls.map((img, index) => (
            <div key={index} className="uploaded-file">
              <span>{img.name}</span>
              <Button type="link" onClick={() => openImageViewer(index)}>
                <EyeOutlined />
              </Button>
              <Button type="link" onClick={() => resetUpload(index)}>
                <CloseOutlined />
              </Button>
            </div>
          ))}
        </div>
        <div className="upload-buttons">
          {imgUrls.length > 0 && (
            <>
              <Button type="primary" danger onClick={() => setImgUrls([])}>
                Reset
              </Button>
              <Button type="primary" onClick={downloadAllFiles}>
                Download All
              </Button>
              <Button type="primary" onClick={Onsubmit}>
                Submit All
              </Button>
            </>
          )}
        </div>
      </div>
      <Modal
        title="Image Viewer"
        visible={viewModalVisible}
        onOk={closeImageViewer}
        onCancel={closeImageViewer}
        width={800}
        footer={null}
      >
        {imgUrls.length > 0 && (
          <img
            src={imgUrls[currentImageIndex].data}
            alt={imgUrls[currentImageIndex].name}
            style={{ width: '100%' }}
          />
        )}
      </Modal>
      <ToastContainer/>
    </div>
  );
};
 
export default UploadFileComponent;