import axios from 'axios';

import Embed from '@editorjs/embed';
import LinkTool from '@editorjs/link';
import ImageTool from '@editorjs/image';
import Header from '@editorjs/header';
import Quote from '@editorjs/quote';
import Marker from '@editorjs/marker';
import Delimiter from '@editorjs/delimiter';
import InlineCode from '@editorjs/inline-code';
import SimpleImage from '@editorjs/simple-image';

const Config = () => {
    return {
        holder: 'editorjs',
        placeholder: 'Nội dung bài viết',
        tools: {
            embed: Embed,
            marker: Marker,
            linkTool: LinkTool,
            image: {
                class: ImageTool,
                config: {
                    uploader: {
                        uploadByFile(file) {
                            return new Promise((resolve, reject) => {
                                const reader = new FileReader();
                                reader.readAsDataURL(file);
                                reader.onload = () => {
                                    const base64String = reader.result;
                                    console.log(base64String);
                                    resolve({
                                        success: 1,
                                        file: {
                                            url: base64String,
                                        },
                                    });
                                };
                                reader.onerror = (error) => reject(error);
                            });
                        },
                    },
                },
                initData: null,
                data: {
                    withBorder: false,
                    withBackground: false,
                    stretched: true,
                },
            },
            header: {
                class: Header,
                shortcut: 'CMD+SHIFT+H',
                config: {
                    defaultLevel: 1,
                },
            },
            quote: {
                class: Quote,
                inlineToolbar: true,
                shortcut: 'CMD+SHIFT+O',
            },
            delimiter: Delimiter,
            inlineCode: InlineCode,
            simpleImage: SimpleImage,
        },
    };
};

export default Config;
