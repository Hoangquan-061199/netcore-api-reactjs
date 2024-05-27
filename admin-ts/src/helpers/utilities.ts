export const getImageDomain : any = (urlPicture : string | undefined, urlImageOrNull: string | null) => {
    if (urlPicture) {
        if (urlPicture.startsWith('http')) {
            return urlPicture;
        } else {
            return `${import.meta.env.VITE_BASE_URL_ROOT}${urlPicture}`;
        }
    }
    return urlImageOrNull;
};