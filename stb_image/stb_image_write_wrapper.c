// stb_image_write_wrapper.c
#define STB_IMAGE_WRITE_IMPLEMENTATION
#include "stb_image_write.h"

#ifdef __cplusplus
extern "C" {
#endif

int write_jpg(const char* filename, int width, int height, int comp, const void* data, int quality)
{
    return stbi_write_jpg(filename, width, height, comp, data, quality);
}

#ifdef __cplusplus
}
#endif
