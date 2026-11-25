import { Alert, Button, Platform, StyleSheet, Text, View } from 'react-native';
import { useEffect, useState } from 'react';
import {
  useAudioRecorder,
  AudioModule,
  RecordingPresets,
  setAudioModeAsync,
  useAudioRecorderState,
  useAudioPlayer
} from 'expo-audio';
import { AudioPlayer } from './AudioPlayer';



export default function App() {
  const audioRecorder = useAudioRecorder(RecordingPresets.HIGH_QUALITY);
  const [audioUri, setAudioUri] = useState<string | null>(null);
  const [recordingStatus, setRecordingStatus] = useState<boolean>(false);

  const recording = audioRecorder.getStatus().isRecording;

  useEffect(() => {
    return () => {
    //   if (recording) {
        audioRecorder.stop();
        setRecordingStatus(false)
    //   }
    };
  }, []);


  const record = async () => {

    let status = await AudioModule.getRecordingPermissionsAsync();
    if(!status.granted){
        status = await AudioModule.requestRecordingPermissionsAsync();
    }

    if(status.granted){
        await setAudioModeAsync({
            playsInSilentMode: true,
            allowsRecording: true,
            allowsBackgroundRecording: true,
        });
        await audioRecorder.prepareToRecordAsync();
        await audioRecorder.record();

        setRecordingStatus(true);
    }else{
        Alert.alert('Permission to access microphone was denied');
    }

    
  };

  const stopRecording = async () => {
    // The recording will be available on `audioRecorder.uri`.
    await audioRecorder.stop();

    setAudioUri(audioRecorder.uri);
    setRecordingStatus(false)
  };

  useEffect(() => {
    (async () => {
      const status = await AudioModule.requestRecordingPermissionsAsync();
      if (!status.granted) {
        Alert.alert('Permission to access microphone was denied');
      }

      setAudioModeAsync({
        playsInSilentMode: true,
        allowsRecording: true,
        allowsBackgroundRecording: true,
      });
    })();
  }, []);



    

  return (
    <View style={styles.container}>
      <Text style={styles.status}>Status: {recording ? "Recording":"ready for recording"}</Text>
      <Button
        title={recordingStatus ? 'Stop Recording' : 'Start Recording'}
        onPress={recordingStatus ? stopRecording : record}
      />
      {audioUri && (
        <Text style={styles.uriText}>
          {audioUri}
        </Text>
      )}

      <AudioPlayer recordingUri={audioUri} />
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center',
    padding: 20,
  },
  status: {
    fontSize: 18,
    marginBottom: 20,
  },
  uriText: {
    marginTop: 20,
    fontSize: 18,
    color: 'gray',
    textAlign: 'center',
    userSelect: 'all'
  },
});
