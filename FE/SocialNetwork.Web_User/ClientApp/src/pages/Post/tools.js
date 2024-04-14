import Embed from '@editorjs/embed';

import LinkTool from '@editorjs/link';
import ImageTool from '@editorjs/image';

import Header from '@editorjs/header';
import Quote from '@editorjs/quote';
import Marker from '@editorjs/marker';

import Delimiter from '@editorjs/delimiter';
import InlineCode from '@editorjs/inline-code';
import SimpleImage from '@editorjs/simple-image';

export const Config = {
    embed: Embed,
    marker: Marker,
    linkTool: LinkTool,
    image: {
        class: ImageTool,
        initData: null,
        data: {},
        withBorder: false,
        withBackground: false,
    },
    header: Header,
    quote: Quote,
    delimiter: Delimiter,
    inlineCode: InlineCode,
    simpleImage: SimpleImage,
};
