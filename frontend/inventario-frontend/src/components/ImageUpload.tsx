import { Box, FileUpload, Text } from "@chakra-ui/react";
import {
  Control,
  Controller,
  FieldError,
  FieldValues,
  Path,
} from "react-hook-form";

interface Props<T extends FieldValues> {
  name: Path<T>;
  control: Control<T>;
  error?: FieldError;
}
export const ImageUpload = <T extends FieldValues>({
  name,
  control,
  error,
}: Props<T>) => {
  return (
    <>
      <Controller
        name={name}
        control={control}
        render={({ field }) => {
          return (
            <FileUpload.Root
              id={name}
              maxW="xl"
              alignItems="stretch"
              maxFiles={1}
              accept="image/*"
            >
              <FileUpload.HiddenInput
                onChange={(event) => {
                  const target = event.target as HTMLInputElement;
                  // Convertimos FileList a Array<File>
                  const files = target.files ? Array.from(target.files) : [];
                  field.onChange(files);
                }}
              />
              <FileUpload.Dropzone>
                <FileUpload.DropzoneContent>
                  <Box>Drag and drop files here</Box>
                  <Box color="fg.muted">.png, .jpg up to 5MB</Box>
                </FileUpload.DropzoneContent>
              </FileUpload.Dropzone>
              <FileUpload.List />
            </FileUpload.Root>
          );
        }}
      />
      {error && <Text color="red.500">{error.message}</Text>}
    </>
  );
};
